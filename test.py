from bottle import Bottle, run, post, request, response
import subprocess
import json
import urllib2

app = Bottle()
@app.route('/')
def testing():
	return "Success"
@app.route('/displaytweet/<user>')
def get_tweet(user):
	try:
		message = urllib2.urlopen("http://192.168.0.98:8080/api/twitter/%s" %user).read()
		message = message.replace('\n', ' ')
		print(message)
		f = open('/Users/adirgozlan/arduino_cmd/src/Hackaton_arduino.ino', 'r')
		replacement = 'char str[] = "%s";\n' %(message)
		lines = f.readlines()
		lines[0] = replacement
		lines[1] = '//\n'
		lines[2] = '//\n'
		lines[3] = '//\n'    # n is the line number you want to edit; subtract 1 as indexing of list starts from 0
		f.close()   # close the file and reopen in write mode to enable writing to file; you can also open in append mode and use "seek", but you will have some unwanted old data if the new data is shorter in length.

		f = open('/Users/adirgozlan/arduino_cmd/src/Hackaton_arduino.ino', 'w')
		f.writelines(lines)
		# do the remaining operations on the file
		f.close()


		response = 'success({"message":'

		subprocess.call(['/usr/local/bin/platformio', 'run', '-t', 'upload'], cwd='/Users/adirgozlan/arduino_cmd/')
		subprocess.call(['/usr/local/bin/platformio', 'run', '--target', 'clean'], cwd='/Users/adirgozlan/arduino_cmd/')
		return response + '\"'+ message + '\"' + '})'
	except:
		response = "error({'message':"
		message = "Oops! No such user."
		return response + '\''+ message + '\'' + "})"



run(app, host='localhost', port=8083)

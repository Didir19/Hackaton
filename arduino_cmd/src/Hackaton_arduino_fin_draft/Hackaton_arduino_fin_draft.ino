
#include <LiquidCrystal.h>
#include <string.h>
#define LINE_SIZE (16)
LiquidCrystal lcd(8, 9, 4, 5, 6, 7);

void setup() {
  // set up the LCD's number of columns and rows:
  Serial.begin(9600);
  lcd.begin(16, 2);
  // Print a message to the LCD.
  lcd.print("Hello, World");
  Serial.println("Test");
  lcd.setCursor(0, 1);
  lcd.print("Welcome to ITC");
  delay(1000);
}

void loop() {
  for (int positionCounter = 0; positionCounter < 13; positionCounter++) {
    // scroll one position left:
    lcd.scrollDisplayLeft();
    // wait a bit:
    delay(150);
  }
  lcd.clear();
  char str[] ="If you are not cleaning the classroom in 10 minutes I will call Oren!";
  char * pch;
  int cur_line = 0;
  int cur_pos = 0;
  pch = strtok (str," ,.-");
  while (pch != NULL)
  {
    lcd.setCursor(cur_pos, cur_line);
    if (strlen(pch) + cur_pos + 1 < LINE_SIZE) {
      lcd.setCursor(cur_pos, cur_line);
      lcd.print(pch);
      lcd.print(" ");
      cur_pos += strlen(pch);
      cur_pos++;
      
    } else if (cur_line == 0) {
      cur_line += 1;
      cur_pos = 0;
      lcd.setCursor(cur_pos, cur_line);
      lcd.print(pch);
      lcd.print(" ");
      cur_pos += strlen(pch);
      cur_pos++;
    } else {
      delay(250);
      lcd.clear();
      cur_line += 1;
      cur_line = cur_line % 2;
      cur_pos = 0;
      lcd.setCursor(cur_pos, cur_line);
      lcd.print(pch);
      lcd.print(" ");
      cur_pos += strlen(pch);
      cur_pos++;
      
      
    }
    pch = strtok (NULL, " ");
    delay(300);
  }
  for (int positionCounter = 0; positionCounter < 13; positionCounter++) {
    // scroll one position left:
    lcd.scrollDisplayRight();
    delay(150);
    Serial.println("test");
  }
  lcd.clear();
  delay(5000);
}

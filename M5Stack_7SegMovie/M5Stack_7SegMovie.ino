#include <Wire.h>
#include <M5Stack.h>

void setup() {
  M5.begin();
  M5.Lcd.drawJpgFile(SD, "/7SegMovie.jpg");
  File file = SD.open("/7SegMovie.dat", "r");
  Wire.begin();
  for (int k = 0; k < 4400; ++k) {
    for (int j = 0; j < 8; ++j) {
      Wire.beginTransmission(0x70 + j);
      Wire.write(0x21);
      Wire.endTransmission();
      Wire.beginTransmission(0x70 + j);
      Wire.write(0x00);
      for (int i = 0; i < 16; ++i) Wire.write(file.read());
      Wire.endTransmission();
      Wire.beginTransmission(0x70 + j);
      Wire.write(0x81);
      Wire.endTransmission();
    }
    delay(26);
  }
}

void loop() {
}

#define Echo_EingangsPin 7
#define Trigger_AusgangsPin 8

long distance;
long duration;

int photoresistor = A1;

int Analog_Input = A2;
int Digital_Input = 3;

int Led_Red = 12;
int Led_Green = 11;

int Buzzer = 13;

int incomingByte[2];

int input;
int ledPin = 5;
bool GreenLED = true;

void setup() {
  // put your setup code here, to run once:
  pinMode(Trigger_AusgangsPin, OUTPUT);
  pinMode(Echo_EingangsPin, INPUT);

  pinMode(Analog_Input, INPUT);
  pinMode(Digital_Input, INPUT);

  pinMode(Led_Red, OUTPUT);
  pinMode(Led_Green, OUTPUT);
  digitalWrite(Led_Red, LOW);
  digitalWrite(Led_Green, LOW);

  pinMode(Buzzer, OUTPUT);

  Serial.begin(9600);

  //LED:
  /*pinMode(ledPin,OUTPUT);
  digitalWrite(ledPin,LOW);*/
}

void loop() {
  
  delay(100);

  digitalWrite(Trigger_AusgangsPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(Trigger_AusgangsPin, LOW);

  duration = pulseIn(Echo_EingangsPin, HIGH);
  distance = duration / 58.2;

  Serial.println(distance);

  int val = analogRead(photoresistor);
  float voltage = val * (5.0/1023) * 1000;
  float resistance = 10000 * (voltage / (5000.0 - voltage));
  if (voltage < 230)
  {
    Serial.println("LightsOn");
  }


  float Analog;
  int Digital;

  Analog = analogRead(Analog_Input) * (5.0 / 1023.0);
  Digital = digitalRead(Digital_Input);

  if (Digital == 1)
  {
    Serial.println("PickUpPowerUp");
  }

  // Change led:
  if(Serial.available() > 0)
  {
    while(Serial.peek() == 'L'){
      Serial.read();
      incomingByte[0] = Serial.parseInt();

      if (incomingByte[0] == 1){
        LedGreen();
      }
      else if (incomingByte[0] == 2){
        LedRed();
      }
    }

    char Input = Serial.read();
    if (Input == 'B'){
      BuzzerSound();
    }

    /*input = Serial.parseInt();
    if (input == 1){
      LedGreen();
    }
    else if (input == 2){
      LedRed();
    }
    else{
      BuzzerSound();
    }*/
  }
}

void LedGreen()
{
  digitalWrite(Led_Red, LOW);
  digitalWrite(Led_Green, HIGH);
  BuzzerSound();
}

void LedRed()
{
  digitalWrite(Led_Green, LOW);
  digitalWrite(Led_Red, HIGH);
  BuzzerSound();
}

void BuzzerSound()
{
  digitalWrite(Buzzer, HIGH);
  delay(10);
  digitalWrite(Buzzer, LOW);
}
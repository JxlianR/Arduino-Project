#define Echo_EingangsPin 7
#define Trigger_AusgangsPin 8

long distance;
long duration;

int photoresistor = A1;

int Analog_Input = A2;
int Digital_Input = 3;

int input;
int ledPin = 5;
bool LED = false;

void setup() {
  // put your setup code here, to run once:
  pinMode(Trigger_AusgangsPin, OUTPUT);
  pinMode(Echo_EingangsPin, INPUT);

  pinMode(Analog_Input, INPUT);
  pinMode(Digital_Input, INPUT);

  Serial.begin(9600);

  //LED:
  /*pinMode(ledPin,OUTPUT);
  digitalWrite(ledPin,LOW);*/
}

void loop() {
  
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
  delay(100);


  float Analog;
  int Digital;

  Analog = analogRead(Analog_Input) * (5.0 / 1023.0);
  Digital = digitalRead(Digital_Input);

  if (Digital == 1)
  {
    Serial.println("PickUpPowerUp");
  }

  // Change led:
  /*delay(100);
  if(Serial.available()>0)
  {
      input = Serial.read();
      digitalWrite(ledPin, input);
  }*/
}
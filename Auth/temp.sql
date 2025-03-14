CREATE TABLE "Event" (
  "id" integer PRIMARY KEY,
  "location" string,
  "created_at" timestamp,
  "user_id" integer
);

CREATE TABLE "UserEvent" (
  "id" integer PRIMARY KEY,
  "event_id" integer NOT NULL,
  "user_id" integer NOT NULL
);

CREATE TABLE "Users" (
  "id" integer PRIMARY KEY,
  "username" varchar,
  "created_at" timestamp,
  "password" varchar
);

CREATE TABLE "WeatherData" (
  "id" integer PRIMARY KEY,
  "data" nvarchar(max)
);

ALTER TABLE "UserEvent" ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("id");

ALTER TABLE "UserEvent" ADD FOREIGN KEY ("event_id") REFERENCES "Event" ("id");

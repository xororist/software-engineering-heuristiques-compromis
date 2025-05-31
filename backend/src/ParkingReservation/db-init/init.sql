CREATE TABLE IF NOT EXISTS "Users" (
                                       "Id" uuid PRIMARY KEY,
                                       "Role" integer NOT NULL
);

CREATE TABLE IF NOT EXISTS "ParkingLots" (
                                             "Id" SERIAL PRIMARY KEY,
                                             "Row" char(1) NOT NULL,
    "Column" integer NOT NULL
    );

CREATE TABLE IF NOT EXISTS "Reservations" (
                                              "Id" uuid PRIMARY KEY,
                                              "UserId" uuid NOT NULL,
                                              "ParkingLotId" integer,
                                              "BeginningOfReservation" timestamp with time zone NOT NULL,
                                              "EndOfReservation" timestamp with time zone NOT NULL,
                                              "HasBeenConfirmed" boolean NOT NULL DEFAULT false,
                                              "HasBeenCancelled" boolean NOT NULL DEFAULT false,
                                              FOREIGN KEY ("UserId") REFERENCES "Users"("Id"),
    FOREIGN KEY ("ParkingLotId") REFERENCES "ParkingLots"("Id")
    );
INSERT INTO "Users" ("Id", "Role") VALUES
                                       ('a1b2c3d4-e5f6-7890-1234-567890abcdef', 0),
                                       ('b2c3d4e5-f6a1-b2c3-d4e5-f6a1b2c3d4e5', 1),
                                       ('c3d4e5f6-a1b2-c3d4-e5f6-a1b2c3d4e5f6', 0);

-- Insérer les places de parking par défaut
INSERT INTO "ParkingLots" ("Row", "Column")
SELECT chr(ascii('A') + generate_series(0, 5)), generate_series(0, 10)
    ON CONFLICT DO NOTHING;
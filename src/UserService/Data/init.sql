CREATE TABLE IF NOT EXISTS "Subscriptions" (
    "Id" SERIAL PRIMARY KEY,
    "Type" VARCHAR(50),
    "StartDate" TIMESTAMP,
    "EndDate" TIMESTAMP
);

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100),
    "Email" VARCHAR(100),
    "SubscriptionId" INT REFERENCES "Subscriptions"("Id")
);

INSERT INTO "Subscriptions" ("Id", "Type", "StartDate", "EndDate") VALUES
(1, 'Free',  '2022-05-17 15:28:19', '2099-01-01 00:00:00'),
(2, 'Super', '2022-05-18 15:28:19', '2022-08-18 15:28:19'),
(3, 'Trial', '2022-05-19 15:28:19', '2022-06-19 15:28:19'),
(4, 'Free',  '2022-05-20 15:28:19', '2099-01-01 00:00:00'),
(5, 'Trial', '2022-05-21 15:28:19', '2022-06-21 15:28:19'),
(6, 'Super', '2022-05-22 15:28:19', '2023-05-22 15:28:19'),
(7, 'Super', '2022-05-23 15:28:19', '2023-05-23 15:28:19');

INSERT INTO "Users" ("Id", "Name", "Email", "SubscriptionId") VALUES
(1, 'John Doe',     'john@example.com', 2),
(2, 'Mark Shimko',  'mark@example.com', 5),
(3, 'Taras Ovruch', 'taras@example.com', 6);

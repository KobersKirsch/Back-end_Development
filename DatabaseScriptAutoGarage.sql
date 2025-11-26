CREATE TABLE CarOwner (
	OwnerId int IDENTITY(1,1) NOT NULL,
	OwnerName varchar(255),
	CONSTRAINT PK_CarOwner PRIMARY KEY (OwnerId)
);
GO
CREATE TABLE Vehicle (
	VehicleId int IDENTITY(1,1) NOT NULL,
	OwnerId int NOT NULL,
	LicensePlate nvarchar(8),
	Description nvarchar(max),
	TowingWeight int NULL,
	CONSTRAINT PK_Vechile PRIMARY KEY (VehicleId),
	FOREIGN KEY (OwnerId) REFERENCES CarOwner(OwnerId)
);

INSERT INTO CarOwner (OwnerName) VALUES ('Martijn Theeuwen');
INSERT INTO CarOwner (OwnerName) VALUES ('Luca Bosch');
INSERT INTO CarOwner (OwnerName) VALUES ('Henk Jan');
INSERT INTO CarOwner (OwnerName) VALUES ('Rico Verhoeven');

INSERT INTO Vehicle (OwnerId, LicensePlate, Description, TowingWeight) VALUES (3,'66-KK-K6', 'Red Ford Focus', NULL);
INSERT INTO Vehicle (OwnerId, LicensePlate, Description, TowingWeight) VALUES (2,'VP-00-EZ', 'Black VolksWagon e-tron', 800);
INSERT INTO Vehicle (OwnerId, LicensePlate, Description, TowingWeight) VALUES (4,'09-KF-7T', 'Blue Mazda 2', NULL);
INSERT INTO Vehicle (OwnerId, LicensePlate, Description, TowingWeight) VALUES (1,'VC-BN-P0', 'White Lorry Van', 4000);
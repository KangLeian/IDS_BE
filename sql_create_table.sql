CREATE TABLE stat(
    id INT NOT NULL,
    name VARCHAR(10),
    PRIMARY KEY(id)
)

CREATE TABLE payments(
	id INT NOT NULL PRIMARY KEY,
    productID VARCHAR(100),
    productName VARCHAR(100),
    amount VARCHAR(100),
    customerName VARCHAR(100),
    statID INT FOREIGN KEY REFERENCES stat(id),
    transactionDate DATE,
    createBy VARCHAR(100),
    createOn DATE,
);
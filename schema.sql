CREATE DATABASE aspbasic;
USE aspbasic;

CREATE TABLE customers ( 
     customerId VARCHAR(6) PRIMARY KEY,
     customerName VARCHAR(255), 
     customerEmail VARCHAR(255)
);

CREATE TABLE studyguides (
    studyGuideCode INT,
    studyGuideName VARCHAR(255),
    price DECIMAL DEFAULT 0.0
);

CREATE TABLE orders (
    orderId INT PRIMARY KEY,
    customerId VARCHAR(6),
    fulfilled BIT DEFAULT 0,
    CONSTRAINT FK_CustomerId FOREIGN KEY (customerId)     
        REFERENCES customers (customerId)     
);

CREATE TABLE orderStudyguides (
    orderId INT, 
    studyGuideCode INT,
    CONSTRAINT FK_OrderId FOREIGN KEY (orderId)     
        REFERENCES orders (orderId)     
        ON DELETE CASCADE    
        ON UPDATE CASCADE    
);


GO
-- CREATE DATABASE aspbasic;
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
    orderId INT IDENTITY(1,1) PRIMARY KEY,
    customerId VARCHAR(6),
    fulfilled BIT DEFAULT 0,
    dateFulfilled  DATETIME2,
    studyGuidesOrdered VARCHAR(255)
    CONSTRAINT FK_CustomerId FOREIGN KEY (customerId)     
        REFERENCES customers (customerId)     
);

-- ideally we would use a join table here

-- CREATE TABLE orderStudyguides (
--     orderId INT, 
--     studyGuideCode INT,
--     CONSTRAINT FK_OrderId FOREIGN KEY (orderId)     
--         REFERENCES orders (orderId)     
--         ON DELETE CASCADE    
--         ON UPDATE CASCADE    
-- );

INSERT INTO CUSTOMERS(customerId, customerName, customerEmail) VALUES ('ABC123','CFONE CLONE','cfone@email.com'); 
INSERT INTO CUSTOMERS(customerId, customerName, customerEmail) VALUES('XYZ123','CFFIVE CLFIVE','cffive@email.com'); 
INSERT INTO CUSTOMERS(customerId, customerName, customerEmail) VALUES ('ABC789','CFSIX CLSIX','cfsix@email.com') ; 


INSERT INTO ORDERS(customerId, studyGuidesOrdered) VALUES('ABC123','5,6');
INSERT INTO ORDERS(customerId, studyGuidesOrdered)  VALUES('XYZ123', '6');
INSERT INTO ORDERS(customerId, studyGuidesOrdered)  VALUES('ABC789','1,6');

INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(1,'STUDY GUIDE ONE',10.00);
INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(2,'STUDY GUIDE ONE',10.00);
INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(3,'STUDY GUIDE ONE',10.00);
INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(4,'STUDY GUIDE ONE',10.00);
INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(5,'STUDY GUIDE ONE',10.00);
INSERT INTO studyguides(studyGuideCode, studyGuideName, price) VALUES(6,'STUDY GUIDE ONE',0.00);


GO
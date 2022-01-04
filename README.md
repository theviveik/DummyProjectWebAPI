# DummyProjectWebAPI

You have to add the database connection string into the appsetting.json file
1. change Password=***** to your database password

Open Package Manager Console and run the below command: 

1. ADD-MIGRATION INIT -PROJECT DataAccessLayer.DataModel

2. UPDATE-DATABASE


After run the mirgation you can run the Web API application, You can web API UI display as Swagger repersentation. you can add, update, view and detete the records base of Swagger UI.
<!DOCTYPE html>
<html>
<body>

<h1>CMPG_323_Project_2_-_38336529</h1>
<h1>Projects and there Reposotories</h1>

<h2>Project Description</h2>
<p>The Creation of a API, used to manage a data source.</p>
<p>The API can be used to manage the given data source in a quick, secure and effective manner.</p>

<h2>Project Manual</h2>
<p>The Project will not allow any unAuthorised access to the endpoints.</p>
<p>The User will first have to register to receive their API Token which is used to authenticate your login to access the API endpoints.</p>
<p>After the Register/Login both work, Register for first time users and login for returning users you can start using the API properly.</p>

<h2>For Customer you have multiple endpoints:<h2>

<img src="/Images/image.png" alt="Customer Endpoints">

<h3>Customer Enpoint details</h3>
<ul>
  <li>Get All Customers</li>
  <li>Get a specific Customer which needs a customer ID passed to the endpoint.</li>
  <li>Create a Customer which needs a Title, Name, Surname and Cellphone to be passed to the endpoint.</li>
  <li>Update a Customer which needs a ID of the customer to update and the needed info of what to change.</li>
  <li>Delete a Customer which needs a ID of the customer to delete.</li>
</ul>  

<h2>For Order you have multiple endpoints:<h2>

<img src="/Images/image-1.png" alt="Order Endpoints">

<h3>Order Enpoint details</h3>
<ul>
  <li>Get All Orders</li>
  <li>Get a specific Order which needs a Order ID passed to the endpoint.</li>
  <li>Create a Order which needs a Orderdate, CustomerID and a deliveryAddress to be passed to the endpoint.</li>
  <li>Update a Order which needs a ID of the Order to update and the needed info of what to change.</li>
  <li>Delete a Order which needs a ID of the Order to delete.</li>
  <li>Get Customer Orders which get all the orders of a specific customer, this endpoint needs a CustomerID passed to it.</li>
</ul>  

<h2>For OrderDetails you have multiple endpoints:<h2>

<img src="/Images/image-2.png" alt="OrderDetails Endpoints">

<h3>OrderDetails Enpoint details</h3>
<ul>
  <li>Get All OrderDetails</li>
  <li>Get a specific OrderDetails which needs a OrderDetails ID passed to the endpoint.</li>
  <li>Create a OrderDetail which needs a OrderID, ProductID, Quantity and a discount which is optional to be passed to the endpoint.</li>
  <li>Update a OrderDetail which needs a ID of the OrderDetails to update and the needed info of what to change.</li>
  <li>Delete a OrderDetail which needs a ID of the OrderDetail to delete.</li>
  <li>Get all Products of a Order, which needs a Order ID to be passed.</li>
</ul>  

<h2>For Product you have multiple endpoints:<h2>

<img src="/Images/image-3.png" alt="Product Endpoints">

<h3>Product Enpoint details</h3>
<ul>
  <li>Get All Products</li>
  <li>Get a specific Product which needs a Product ID passed to the endpoint.</li>
  <li>Create a Product which needs a Name, Description and units in stock to be passed to the endpoint.</li>
  <li>Update a Product which needs a ID of the Product to update and the needed info of what to change.</li>
  <li>Delete a Product which needs a ID of the Product to delete.</li>
</ul>  

<h2>All the Data Transfer Objects:</h2>
<img src="/Images/image-4.png" alt="Al DTO's">

<h2>References:</h2>
<p>Saini, S. 2023. Use C# and Build an ASP.NET Core Web API with Entity Framework Core, SQL Server, Authentication & Authorization | .NET 7. https://www.udemy.com/course/build-rest-apis-with-aspnet-core-web-api-entity-framework/learn/lecture/36980480#overview </p>
<p>Zyl, J.V. 2023. API and Database Development. https://exceedit.tech/</p>

<h1>Website Access:</h1>
<p>https://project2cmpg323api20230828132747.azurewebsites.net/swagger/index.html</p>

</body>
</html>

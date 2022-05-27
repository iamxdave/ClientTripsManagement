# ClientTripsManagement
Client trips management through <b>REST API</b> by <b>EntityFrameworkCore</b>.<br>
<br>
<b>The application meets the assumptions of SOLID and DI</b>

<hr>

## How does it work

  Application is created for a company which orgianizes client trips to different countries.
  The database I use is presented below.

<p align="center">
  <img src=https://user-images.githubusercontent.com/74014874/170800079-d6d24264-fa54-46fb-a99e-cd7d55b5f51a.png
   >
</p>

<hr>

  API allows you to maintain certain operation and put or update tables in the database e.g:

1. Return a list of trips in descending order on the start date by responding to the
<p align="center">
  <b>HTTP GET request to /api/trips
</p>
  with the data of the following form:
 <p align="center">
  <br />
 <img src=https://user-images.githubusercontent.com/74014874/170800426-2f784952-8128-4195-bc94-ec301dea5e91.png
  >
</p>
2. Delete a client's data by responding to the
<p align="center">
  <b>HTTP DELETE request to /api/clients/{idClient}
</p> 
  API checks client's connections to trips and returns an error if the client has any bindings.
3. Add a client for the trip by responding to the 
<p align="center">
  <b>HTTP POST request to /api/trips/{idTrip}/clients</b>
</p>
  with the data of the following form:
  
<p align="center">
  <br />
  <img src=https://user-images.githubusercontent.com/74014874/170800186-22bff4f2-0356-4bf9-ad0f-259b57d606c5.png
   >
</p>

  **PaymentDate may be null for clients who haven't payed for the trip yet and the RegisteredAt column is equal to the present tense.**
  
<hr>
  
  ## API meets the following requirements:
  1. Uses DTOs models for returns and received by the endings.
  2. Checks if a person with such PESEL exists.
  3. Checks if a client in POST request is signed up for a trip.
  4. Checks if a trip in POST request exists.
  5. Returns error messages for individual situations.


  **API is connected to my database by default and to set up yours you need to change ConnectionString in the file appsettings.json**

<hr>

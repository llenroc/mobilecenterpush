And iOS/Android app as well as Azure Function project that makes it easier to send push notifications through the Mobile Center API's.  

## Visual Studio Requirements

### Visual Studio for Mac

This solution requires Visual Studio for Mac Version 7.1 Build 1294 or later.

Earlier versions of VSMac don't support Shared Project references from .NET Standard libraries.

### Visual Studio for PC

This solution requires Visual Studio for PC Version 15.3 or later.

Earlier versions of VS don't support Azure Functions.

### Why Mobile Center push is awesome

Mobile Center push maps the FCM (google firebase) and APNS (apple push notifications) registration tokens to the installationID of your app. There is a unique ID for each time an app gets installed on a user's device through mobile center. With this project you can use the mobile center APIs to send out push notifications to installationIDs. 

## Getting Started

### 1. Create a Mobile Center App for Android and iOS 
### 2. Follow the steps to set up push notifications (client side) through the Mobile Center Dashboard
### 3. Create an Azure Function App
### 4. Create a CosmosDB App 
### 5. Fill out the required Keys in the MobileCenterPush and MobileCenterPush.Shared projects
### 6. Publish the Azure functions 
### 7. You can now use the 3 function endpoints to register users with a custom tag, or send a push notification to a specific audience (tag) or list of users


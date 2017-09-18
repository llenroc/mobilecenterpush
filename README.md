## Push Notifications with VS Mobile Center
An iOS/Android app as well as Azure Function project that makes it easier to send push notifications through the Mobile Center API's.  

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

### Mobile Center setup:
1. Create a Mobile Center App for Android and iOS 
2. Follow the steps to set up push notifications (client side) through the Mobile Center Dashboard
  - This step will involve creating an app registration in Google's Firebase console for Android, as well as creating an APNS     certificate for iOS
  - These platform specific steps are required to send push notifications to your devices: https://docs.microsoft.com/en-us/mobile-center/push/

### Azure Functions & CosmosDB setup:
- We're going to abstract the calls to the mobile center API's through our own Azure function, which makes it easier to manage the sending of push notifications from our application's backend.

1. Create an Azure Function App
2. Create a an Azure Cosmos DB 
3. Fill out the required Keys in the MobileCenterPush and MobileCenterPush.Shared projects
4. Publish the Azure functions 
5. You can now use the 3 function endpoints to register users with a custom tag, or send a push notification to a specific audience (tag) or list of users


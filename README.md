## Push Notifications with VS Mobile Center
An iOS/Android app as well as Azure Function project that makes it easier to send push notifications through the Mobile Center API's.  

## Visual Studio Requirements

### Visual Studio for Mac

This solution requires Visual Studio for Mac Version 7.1 Build 1294 or later.

Earlier versions of VSMac don't support Shared Project references from .NET Standard libraries.

### Visual Studio for PC

This solution requires Visual Studio for PC Version 15.3 or later.

Earlier versions of VS don't support Azure Functions.

### Why Mobile Center push is awesome and the motivation for this sample

- Mobile Center push maps the FCM (google firebase) and APNS (apple push notifications) registration tokens to the installationID of your app. There is a unique ID for each time an app gets installed on a user's device through mobile center. With this project you can use the mobile center APIs to send out push notifications to installationIDs. 

- However, Mobile Center doens't currently support Tags - which is a feature of Notification Hubs that isn't implemented yet in Mobile Center Push. It does support audiences and custom properties, but the audiences feature was really intended for sending notifications from the portal and not through the API. The custom properties are also not "Push-specific" - but rather global in mobile center - meaning that they will show in your analytics as well: https://docs.microsoft.com/en-us/mobile-center/push/audiences

- For this reason, we'll be managing our own tags in a simple document-based database that contains the Tag, along with a list of installationIds for devices that are subscribed to that tag. 

## Getting Started

### Mobile Center setup:
1. Create a Mobile Center App for Android and iOS 
2. Follow the steps to set up push notifications (client side) through the Mobile Center Dashboard
  - This step will involve creating an app registration in Google's Firebase console for Android, as well as creating an APNS     certificate for iOS
  - These platform specific steps are required to send push notifications to your devices: https://docs.microsoft.com/en-us/mobile-center/push/
 
3. Create a mobile center API token - https://docs.microsoft.com/en-us/mobile-center/api-docs/

### Azure Functions & CosmosDB setup:
- We're going to abstract the calls to the mobile center API's through our own Azure function, which makes it easier to manage the sending of push notifications from our application's backend.

1. Create an Azure Function App (this step isn't necessarily required because you can publish to a new function app from Visual Studio - but I like to have one running in either case)
  - As for the hosting plan - you can select whatever you like - but I prefer the consumption plan
  
2. Create a an Azure Cosmos DB 
  - We are using CosmosDB to manage our Tags
  - For the cosmosDB collection - choose fixes as opposed to unlimited (with fixed you don't require a partition key)
  
3. Fill out the required Keys in the MobileCenterPush and MobileCenterPush.Shared projects
  - The keys for CosmosDB can be found in the Keys section of your CosmosDB Resource
  - The Azure functions are currently set up as anonymous - but you can set it as a function-level authorization 
  
4. Publish the Azure functions project

5. You can now use the 3 function endpoints to register users with a custom tag, or send a push notification to a specific audience (tag) or list of users. A sample of how to use the API is included in the sample project on the button's click event. 


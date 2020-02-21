# Parking

This project is realized as a bachelor thesis. The aim of this application is to create an application for providing, receiving and processing parking data. Data will be received from the sensors via the MQTT protocol. The application then processes them and prepares them for delivery using the REST API. Actual test version of API with swagger documentation can be found [here](https://testparking-api.azurewebsites.net).

## BUILDS
This repository contains 2 applications. Their working names are DEV-API and DEV-MQTT. Api is used to provide data. MQTTs to receive and store them in a database.

**DEV-API**     

[![Build Status](https://dev.azure.com/trojan-projects/Parking/_apis/build/status/dev-api?branchName=dev)](https://dev.azure.com/trojan-projects/Parking/_build/latest?definitionId=12&branchName=dev)

**DEV-MQTT**

[![Build Status](https://dev.azure.com/trojan-projects/Parking/_apis/build/status/dev-Mqtt?branchName=dev)](https://dev.azure.com/trojan-projects/Parking/_build/latest?definitionId=11&branchName=dev)


"use strict";

var connection = new signalR().HubConnectionBuilder().withUrl("/notificationHub").build();

// Init hub
$(function () {
    connection.start().then(function () {
        console.log("Connected to hub");
    }).catch(function (error) {
        console.err(error.toString());
    })
});
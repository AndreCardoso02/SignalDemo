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

connection.on("OnConnected", function () {
    OnConnected();
});

function OnConnected() {
    var username = $('#hfUsername').val();
    connection.invoke("SaveUserConnection", username).catch(function (err) {
        console.error(err.toString());
    });
}
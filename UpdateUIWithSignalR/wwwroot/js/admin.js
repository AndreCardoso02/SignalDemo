"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/adminHub").build();

$(function () {
    connection.start().then(function () {
        console.log('connected to adminHub');
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("ReceivedMessage", function (message, status) {

});
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/adminHub").build();

$(function () {
    connection.start().then(function () {
        console.log('connected to adminHub');
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("ReceivedMessage", function (type, message, status) {
    $('#trRetrieve td:nth-child(2)').text(message);
    $('#trRetrieve td:nth-child(3)').text(status);
});
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        alert('Connected to dashboard');

        InvokeProducts();
        InvokeSales();
        InvokeCustomers();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});


// -- Send and Receive Products List
function InvokeProducts() {
    connection.invoke("SendProducts").catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedProducts", function (products) {
    BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
    $('#tblProduct tbody').empty();

    var tr;
    $.each(products, function (index, product) {
        tr = $('<tr/>');
        tr.append(`<td>${(index + 1)}</td>`);
        tr.append(`<td>${product.name}</td>`);
        tr.append(`<td>${product.category}</td>`);
        tr.append(`<td>${product.price}</td>`);
        $('#tblProduct').append(tr);
    });
}

// -- Send and Receive Sales
function InvokeSales() {
    connection.invoke("SendSales").catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedSales", function (sales) {
    BindSalesToGrid(sales);
});

function BindSalesToGrid(sales) {
    $('#tblSale tbody').empty();

    var tr;

    $.each(sales, function (index, sale) {
        tr = $('<tr/>');
        tr.append(`<td>${(index + 1)}</td>`);
        tr.append(`<td>${sale.customer}</td>`);
        tr.append(`<td>${sale.amount}</td>`);
        tr.append(`<td>${sale.purchasedOn}</td>`);
        $('#tblSale').append(tr);
    });
}

// Send and Receive Customers
function InvokeCustomers() {
    connection.invoke("SendCustomers").catch(function (err) {
        return console.error(err.toString());
    });
}


connection.on("ReceivedCustomers", function (customers) {
    BindCustomersToGrid(customers);
});

function BindCustomersToGrid(customers) {
    $('#tblCustomer tbody').empty();

    var tr;

    $.each(customers, function (index, customer) {
        tr = $('<tr/>');
        tr.append(`<td>${(index + 1)}</td>`);
        tr.append(`<td>${customer.name}</td>`);
        tr.append(`<td>${customer.gender}</td>`);
        tr.append(`<td>${customer.mobile}</td>`);
        $('#tblCustomer').append(tr);
    });
}
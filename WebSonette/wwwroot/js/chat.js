"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var test = false;

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//Definition de l'action a faire lors de la reception du signal messageSonette
connection.on("messageSonette", function (message) {
    //Attribut a l'element message du code html le message recu dans le signal
    document.getElementById("message").innerHTML = message;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    test = !test;
    
    connection.invoke("SendMessage",test).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
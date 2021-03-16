"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendMessage").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var completeMessage = user + " says: " + message;
    var li = document.createElement("li");
    li.textContent = completeMessage;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendMessage").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
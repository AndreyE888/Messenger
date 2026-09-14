const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5202/chathub")
    .build();

const messagesDiv = document.getElementById("messages");
const input = document.getElementById("messageInput");
const sendButton = document.getElementById("sendButton");

connection.on("ReceiveMessage", (user, message) => {
    const div = document.createElement("div");
    div.className = "message";
    div.textContent = `${user}: ${message}`;
    messagesDiv.appendChild(div);
    messagesDiv.scrollTop = messagesDiv.scrollHeight;

})

sendButton.addEventListener("click", async () => {
    const message = input.value.trim();
    if (!message) return;
    try {
        await connection.invoke("SendMessage", "Пользователь", message);
        input.value = "";
    }
    catch (err) {
        console.error("Ошибка отправки: ", err);
    }
});

connection.start()
    .then( () => console.log("Подключено к SignalR"))
        .catch(err => console.error("Ошибка подключения!", err));
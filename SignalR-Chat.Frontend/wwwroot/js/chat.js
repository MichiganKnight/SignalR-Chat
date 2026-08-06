const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7255/chat", {
    accessTokenFactory: () => {
        return window.currentUserToken;
    }
}).build();

connection.on("ReceiveMessage", (username, message, timestamp) => {
    const div = document.createElement("div");

    div.innerHTML = `
            <div class="message-header">
                <strong>${username}</strong>
                <span>${new Date(timestamp).toLocaleString()}</span>
            </div>

            <div class="message-content">
                ${message}
            </div>
        `;
    
    document.getElementById("messages").appendChild(div);
    
    const messages = document.getElementById("messages");
    
    messages.scrollTop = messages.scrollHeight;
});

connection.on("OnlineUsersUpdated", users => {
    const container = document.getElementById("onlineUsers");
    
    container.innerHTML = "";
    
    users.forEach(user => {
        const div = document.createElement("div");
        
        div.classList.add("online-user");
        
        div.innerHTML = `<span class="status-dot"></span> ${user.username}`;
        
        container.appendChild(div);
    });
});

document.getElementById("sendButton").addEventListener("click", async () => {
    const input = document.getElementById("messageInput");
    const message = input.value.trim();
    
    await connection.invoke("SendMessage", message);
    
    input.value = "";
});

connection.onclose(async () => {
    console.log("Disconnected From Chat"); 
});

connection.start().then(() => {
    console.log("Connected To ChatHub");
}).catch(err => {
    console.error(err);
});
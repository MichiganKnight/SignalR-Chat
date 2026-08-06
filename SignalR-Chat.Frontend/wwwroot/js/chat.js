const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7255/chat", {
    accessTokenFactory: () => {
        return window.currentUserToken;
    }
}).build();

let typingTimeout;

const input = document.getElementById("messageInput");

input.addEventListener("input", async () => {
    await connection.invoke("SendTyping");

    clearTimeout(typingTimeout);

    typingTimeout = setTimeout(() => {

    }, 1500);
});

connection.on("ReceiveMessage", (username, message, timestamp) => {
    const div = document.createElement("div");
    
    div.classList.add("message");

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

connection.on("UserTyping", username => {
    const indicator = document.getElementById("typingIndicator");
    
    indicator.innerText = `${username} is typing...`;
    
    clearTimeout(indicator.timeout);
    
    indicator.timeout = setTimeout(() => {
        indicator.innerText = "";
    }, 1500);
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
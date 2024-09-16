//const connection = new signalR.HubConnectionBuilder()
//    .withUrl("/chatHub")
//    .build();

//// Nhận tin nhắn từ Hub
//connection.on("ReceiveMessage", function (senderId, message) {
//    const msg = `From ${senderId}: ${message}`;
//    const li = document.createElement("li");
//    li.textContent = msg;
//    document.getElementById("messagesList").appendChild(li);
//});

//// Kết nối đến Hub
//connection.start().catch(function (err) {
//    return console.error(err.toString());
//});

//// Gửi tin nhắn từ người gửi (SenderId) đến người nhận (ReceiverId)
//function sendMessage() {
//    const receiverId = document.getElementById("receiverInput").value;
//    const message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessageToUser", receiverId, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    document.getElementById("messageInput").value = ''; // Xóa nội dung sau khi gửi tin nhắn
//}
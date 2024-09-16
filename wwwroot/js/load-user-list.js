document.getElementById('friendSearch').addEventListener('input', function () {
    var input = this.value.toLowerCase();
    var dropdown = document.getElementById('friendDropdown');

    // Xóa nội dung hiện tại của dropdown
    dropdown.innerHTML = '';

    // Gửi yêu cầu fetch đến backend với search term
    fetch(`/Home/GetFriends?search=${input}`)
        .then(response => response.json())
        .then(friends => {
           
            if (friends.length > 0) {
                dropdown.classList.add('show');
                friends.forEach(friend => {
                    var a = document.createElement('a');
                    a.href = '/Home/Chat/'+ friend.id;

                    // Tạo cấu trúc modal tùy chỉnh
                    var contentDiv = document.createElement('div');
                    contentDiv.classList.add('content');

                    var img = document.createElement('img');
                    img.src = '/Hinh/Image/' + friend.img; // Hình ảnh đại diện
                    contentDiv.appendChild(img);

                    var detailsDiv = document.createElement('div');
                    detailsDiv.classList.add('details');

                    var nameSpan = document.createElement('span');
                    nameSpan.textContent = friend.username;
                    detailsDiv.appendChild(nameSpan);

                   

                    contentDiv.appendChild(detailsDiv);
                    a.appendChild(contentDiv);
                    // Thêm sự kiện click để chọn bạn bè
                    a.addEventListener('click', function () {
                        document.getElementById('friendSearch').value = friend.username;
                        dropdown.classList.remove('show');
                    });

                    // Thêm vào dropdown
                    dropdown.appendChild(a);
                });
            } else {
                dropdown.classList.remove('show');
            }
        })
        .catch(error => {
            console.error('Error fetching friends:', error);
        });
});


// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var createRoomBtn = document.getElementById("create-room");

var createRoomModal = document.getElementById("create-room-modal");
createRoomBtn.addEventListener("click", () => {
    console.log("click +");
    createRoomModal.classList.add('active');
});

function closeModal() {
    createRoomModal.classList.remove('active');
}
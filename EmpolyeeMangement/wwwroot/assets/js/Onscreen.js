function updateFileName() {
    const fileInput = document.getElementById('excelFile');
    const fileLabel = document.getElementById('fileLabel');

    if (fileInput.files.length > 0) {
        const fileName = fileInput.files[0].name;
        fileLabel.textContent = fileName;
    } else {
        fileLabel.textContent = 'Click to select Excel file';
    }
}

function previewImage(event) {
    const imageInput = event.target;
    const reader = new FileReader();

    reader.onload = function () {
        const output = document.getElementById('profilePic');
        output.src = reader.result;
    };

    if (imageInput.files && imageInput.files[0]) {
        reader.readAsDataURL(imageInput.files[0]);
    }
}
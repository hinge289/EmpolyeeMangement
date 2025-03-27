function downloadTemplate(button) {
    const fileName = button.getAttribute('data-filename') || 'temp.xlsx';
    const fileUrl = `/Excel/${fileName}`;

    const a = document.createElement('a');
    a.href = fileUrl;
    a.download = fileName;
    a.click();
}
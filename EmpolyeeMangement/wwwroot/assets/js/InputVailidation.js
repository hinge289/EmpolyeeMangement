document.addEventListener('DOMContentLoaded', function() {
  
    document.getElementById('mobileNo').addEventListener('input', function(event) {
      
        event.target.value = event.target.value.replace(/[^0-9]/g, '');
    });
});
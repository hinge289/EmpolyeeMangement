function validateFileType() {
    //  debugger;
      var fileName = document.getElementById("file").value;
      var idxDot = fileName.lastIndexOf(".") + 1;
      var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
      if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
          //TO DO
      }
      else {
          event.target.value = '';
  
          alert("Only jpg / jpeg / png files are allowed!");
  
      }
  
  
  
  }
  
  
  function validatesiz() {
      debugger;
      var fileName = document.getElementById("file");
  
      //alert(fileName.files[0].size);
      if (fileName.files[0].size > 100000 || fileName.files[0].size == 0)  // validation according to file size
      {
  
          fileName.value = null;
          alert("Photo Size Only 100kb !");
  
      }
      else {
  
      }
  }
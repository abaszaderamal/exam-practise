window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
      document.getElementById("header").style.padding = "10px 0";
      document.querySelector(".navbar__logo").style.width = "140px";
      document.getElementById("header").style.backgroundColor = "#212529";
    } else {
      document.getElementById("header").style.padding = "20px 0";
      document.querySelector(".navbar__logo").style.width = "187px";
      document.getElementById("header").style.backgroundColor = "";
      
    }
  }
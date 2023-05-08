// Listen for Click -----------------------------------------------------------

document.addEventListener("click", (e) =>{
    if(e.target.classList.contains("slider-prev")){
        togglePrevious();
        updatePrev();
    }
    else if(e.target.classList.contains("slider-next")){
        toggleNext();
        updateNext();
    }
})



// Rotate the h3 text -----------------------------------------------------------

function togglePrevious(){
    const currentActive = document.querySelector("h3.active");
    const currentIndex = parseInt(currentActive.dataset.index);
    //console.log(currentIndex);

    if(currentIndex - 1 >= 0){
        const previousChild = document.querySelector(`[data-index="${currentIndex-1}"]`);
        //console.log(previousChild)
        currentActive.classList.remove("active");
        previousChild.classList.add("active");
    }
    else{
        const previousChild = document.querySelector(`[data-index="${4}"]`);
        //console.log(previousChild)
        currentActive.classList.remove("active");
        previousChild.classList.add("active");
    }
}


function toggleNext(){
    const currentActive = document.querySelector("h3.active");
    const currentIndex = parseInt(currentActive.dataset.index);
    //console.log(currentIndex);

    if(currentIndex + 1 <= 4){
        const nextChild = document.querySelector(`[data-index="${currentIndex+1}"]`);
        //console.log(nextChild)
        currentActive.classList.remove("active");
        nextChild.classList.add("active");
    }
    else{
        const nextChild = document.querySelector(`[data-index="${0}"]`);
        //console.log(nextChild)
        currentActive.classList.remove("active");
        nextChild.classList.add("active");
    }
}



// Rotate the images -----------------------------------------------------------

function updateNext() {
    // Set the position of each item based on the current index
    const items = document.querySelectorAll('.item');
    items.forEach((item) => {
      let position = parseInt(item.style.order);
      console.log(position);
      if (position + 1 <= 4){
        position ++;
        item.style.order = position;
        console.log(position);
      } else {
        item.style.order = 0;
        console.log(position);
      }

      
    });
    // Apply styles based on the position of each item
    items.forEach((item) => {
        let position2 = parseInt(item.style.order);
      if (position2 == 0) {
        item.classList.add('small1');
        item.classList.remove('small2');
      } else if (position2 == 1) {
        item.classList.add('big1');
        item.classList.remove('small1');
      } else if (position2 == 2) {
        item.classList.add('focus');
        item.classList.remove('big1');
      } else if (position2 == 3) {
        item.classList.add('big2');
        item.classList.remove('focus');
      } else if (position2 == 4) {
        item.classList.add('small2');
        item.classList.remove('big2');
      }
    });
  }

  function updatePrev() {
    // Set the position of each item based on the current index
    const items = document.querySelectorAll('.item');
    items.forEach((item) => {
      let position = parseInt(item.style.order);
      console.log(position);
      if (position - 1 >= 0){
        position --;
        item.style.order = position;
        console.log(position);
      } else {
        item.style.order = 4;
        console.log(position);
      }

      
    });
    // Apply styles based on the position of each item
    items.forEach((item) => {
        let position2 = parseInt(item.style.order);
      if (position2 == 0) {
        item.classList.add('small1');
        item.classList.remove('big1');
      } else if (position2 == 1) {
        item.classList.add('big1');
        item.classList.remove('focus');
      } else if (position2 == 2) {
        item.classList.add('focus');
        item.classList.remove('big2');
      } else if (position2 == 3) {
        item.classList.add('big2');
        item.classList.remove('small2');
      } else if (position2 == 4) {
        item.classList.add('small2');
        item.classList.remove('small1');
      }
    });
  }
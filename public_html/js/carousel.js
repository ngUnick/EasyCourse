// Set Img Attributes ----------------------------

window.addEventListener('load', () => {
    window.images = document.querySelectorAll('ul li img');
    window.slideWidth = images[0].clientWidth;

    console.log(images);
    setImgAttributes(images, slideWidth);

  });


function setImgAttributes(images, slideWidth){
    for (const [position,image] of images.entries() ) {
     
        if (image.classList.contains('active')) {
            image.style.height = '300px';
            image.style.transform = 'translateX(0px)';
            image.style.zIndex = '20';
            continue;
        }
        switch(position){
            // Apply unique attributes to each image based on its position and class
           case 0:
        
                image.style.height = '200px';
                image.style.transform = 'translateX(' + (slideWidth * 1.2) + 'px)';
                image.style.marginTop = '50px';
                image.style.zIndex = 1;
                break;
        
          case 1:
                image.style.height = '250px';
                image.style.transform = 'translateX(' + (slideWidth * 0.7) + 'px)';
                image.style.marginTop = '25px';
                image.style.zIndex = 10;
                break;
        
          case 3:
                image.style.height = '250px';
                image.style.transform = 'translateX(' + (-slideWidth * 0.7) + 'px)';
                image.style.marginTop = '25px';
                image.style.zIndex = 10;
                break;
        
          case 4 :
                image.style.height = '200px';
                image.style.transform = 'translateX(' + (-slideWidth * 1.2) + 'px)';
                image.style.marginTop = '50px';
                image.style.zIndex = 1;
                break;
       }
    }
}

// Toggle Slider ----------------------------

document.addEventListener("click", e => {
    if(e.target.classList.contains("slider-prev")){
        togglePrevious();
    }
    else if(e.target.classList.contains("slider-next")){
        toggleNext();
    }
})


<<<<<<< Updated upstream
function togglePrevious(){
=======

// Rotate the h3 text -----------------------------------------------------------

const togglePrevious = () => {
>>>>>>> Stashed changes
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


const toggleNext = () =>{
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


function temp(images){
    let array = Array.from(images);
    let lastValue = array.pop(); // remove the last value from the array
    array.unshift(lastValue);

    let parentElement = images[0].parentNode; // get the parent element of the first node
    while (parentElement.firstChild) {
    parentElement.removeChild(parentElement.firstChild);
    }
    for (let i = 0; i < array.length; i++) {
    parentElement.appendChild(array[i]);
    }
    let updatedNodeList = document.querySelectorAll('ul li img');

<<<<<<< Updated upstream
    console.log(updatedNodeList);




    const activeImg =  document.querySelector("img.active");
    console.log(activeImg);

    const activeData = parseInt(activeImg.dataset.index);
    console.log(activeData);

    if(activeData - 1 >= 10){
        const nextImg = document.querySelector(`[data-index="${activeData-1}"]`);
        //console.log(nextChild)
        activeImg.classList.remove("active");
        nextImg.classList.add("active");
    }
    else{
        const nextImg = document.querySelector(`[data-index="${14}"]`);
        //console.log(nextChild)
        activeImg.classList.remove("active");
        nextImg.classList.add("active");
    }




    setImgAttributes(updatedNodeList, slideWidth);
}
=======
const updateNext = () => {
    // Set the position of each item based on the current index
    const items = document.querySelectorAll('.item');
    items.forEach((item) => {
      let position = parseInt(item.style.order);
      console.log(position);
      item.style.order = position <=3 ? position+1 : 0;
      console.log(position);
    });
    // Apply styles based on the position of each item
    items.forEach( item => PositionWiseApplyStyle(item) )
  }

const updatePrev = ()=> {
    // Set the position of each item based on the current index
    const items = document.querySelectorAll('.item');
    items.forEach((item) => {
      let position = parseInt(item.style.order);
      console.log(position);
      item.style.order = position >= 1 ? position -1 : 4
      console.log(position);
    });
    // Apply styles based on the position of each item
    items.forEach( item => IndexWiseApplyStyle(item) );
  }

const PositionWiseApplyStyle = item =>
{
  let position2 = parseInt(item.style.order);
        switch(position2) {
      case 0:
        item.classList.add('small1');
        item.classList.remove('small2');
        break;
      case 1:
        item.classList.add('big1');
        item.classList.remove('small1');
        break;
      case 2:
        item.classList.add('focus');
        item.classList.remove('big1');
        break;
      case 3:
        item.classList.add('big2');
        item.classList.remove('focus');
        break;
      case 4:
        item.classList.add('small2');
        item.classList.remove('big2');
        break;
      default:
        console.log('unexpected error')
      }
}

const IndexWiseApplyStyle = item =>
{
  let position2 = parseInt(item.style.order);
        switch(position2) {
      case 0:
        item.classList.add('small1');
        item.classList.remove('big1');
        break;
      case 1:
        item.classList.add('big1');
        item.classList.remove('focus');
        break;
      case 2:
        item.classList.add('focus');
        item.classList.remove('big2');
        break;
      case 3:
        item.classList.add('big2');
        item.classList.remove('small2');
        break;
      case 4:
        item.classList.add('small2');
        item.classList.remove('small1');
        break;
      default:
        console.log('unexpected error')
      }
}
>>>>>>> Stashed changes

// Set Img Attributes ----------------------------

window.addEventListener('load', () => {
    window.images = document.querySelectorAll('ul li img');
    window.slideWidth = images[0].clientWidth;

    console.log(images);
    setImgAttributes(images, slideWidth);

  });


function setImgAttributes(images, slideWidth){
    for (let i = 0; i < images.length; i++) {
        const image = images[i];
    
        // Apply unique attributes to each image based on its position and class
        if (i === 0) {
    
            image.style.height = '200px';
            image.style.transform = 'translateX(' + (slideWidth * 1.2) + 'px)';
            image.style.marginTop = '50px';
            image.style.zIndex = 1;
    
        } else if (i === 1) {
    
            image.style.height = '250px';
            image.style.transform = 'translateX(' + (slideWidth * 0.7) + 'px)';
            image.style.marginTop = '25px';
            image.style.zIndex = 10;
    
        } else if (image.classList.contains('active')) {

            image.style.height = '300px';
            image.style.transform = 'translateX(0px)';
            image.style.zIndex = '20';

        } else if (i === 3) {
    
            image.style.height = '250px';
            image.style.transform = 'translateX(' + (-slideWidth * 0.7) + 'px)';
            image.style.marginTop = '25px';
            image.style.zIndex = 10;
    
        } else if (i === 4) {
    
            image.style.height = '200px';
            image.style.transform = 'translateX(' + (-slideWidth * 1.2) + 'px)';
            image.style.marginTop = '50px';
            image.style.zIndex = 1;
    
        }
        }
}

// Toggle Slider ----------------------------

document.addEventListener("click", (e) =>{
    if(e.target.classList.contains("slider-prev")){
        togglePrevious();
    }
    else if(e.target.classList.contains("slider-next")){
        toggleNext();
    }
})


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
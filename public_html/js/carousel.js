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
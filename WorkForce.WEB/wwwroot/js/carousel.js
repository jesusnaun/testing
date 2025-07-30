// wwwroot/js/carousel.js

window.myApp = window.myApp || {};

window.myApp.initCarousel = function (containerId) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.warn(`Carousel container with ID '${containerId}' not found.`);
        return;
    }

    const slides = container.querySelectorAll('.banner-img');
    const dots = container.querySelectorAll('.dot');
    const prevBtn = container.querySelector('.arrow.left');
    const nextBtn = container.querySelector('.arrow.right');
    let current = 0;
    let intervalId; // Variable para almacenar el intervalo

    function showSlide(index) {
        current = (index + slides.length) % slides.length;

        slides.forEach((img, i) => {
            img.classList.toggle('active', i === current);
        });
        dots.forEach((dot, i) => {
            dot.classList.toggle('active', i === current);
        });
    }

    // Función para avanzar automáticamente
    function autoAdvance() {
        showSlide(current + 1);
    }

    // Iniciar intervalo automático (cada 10 segundos)
    function startAutoPlay() {
        intervalId = setInterval(autoAdvance, 10000);
    }

    // Detener autoplay (para interacciones manuales)
    function pauseAutoPlay() {
        clearInterval(intervalId);
    }

    // Reiniciar autoplay después de interacción
    function resetAutoPlay() {
        pauseAutoPlay();
        startAutoPlay();
    }

    showSlide(current);
    startAutoPlay(); // Iniciar autoplay

    // Event listeners
    if (prevBtn) {
        prevBtn.addEventListener('click', () => {
            showSlide(current - 1);
            resetAutoPlay();
        });
    }
    if (nextBtn) {
        nextBtn.addEventListener('click', () => {
            showSlide(current + 1);
            resetAutoPlay();
        });
    }

    if (dots.length > 0) {
        dots.forEach(dot => {
            dot.addEventListener('click', () => {
                showSlide(parseInt(dot.dataset.slide, 8));
                resetAutoPlay();
            });
        });
    }

    // Pausar al pasar el mouse sobre el carrusel
    container.addEventListener('mouseenter', pauseAutoPlay);
    container.addEventListener('mouseleave', startAutoPlay);

    return {
        dispose: () => {
            if (prevBtn) prevBtn.removeEventListener('click', () => { });
            if (nextBtn) nextBtn.removeEventListener('click', () => { });
            dots.forEach(dot => dot.removeEventListener('click', () => { }));
            container.removeEventListener('mouseenter', pauseAutoPlay);
            container.removeEventListener('mouseleave', startAutoPlay);
            clearInterval(intervalId); // Limpiar intervalo
        }
    };
};
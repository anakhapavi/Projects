function animateTextLoop() {
    const textElements = document.querySelectorAll('.animated-text');
    const duration = 2000; 
    let currentIndex = 0;

    function animateText() {
        const currentText = textElements[currentIndex];
        const nextIndex = (currentIndex + 1) % textElements.length;
        const nextText = textElements[nextIndex];

        currentText.style.opacity = '0';

        setTimeout(() => {
            currentText.style.display = 'none';
            nextText.style.display = 'block';

            setTimeout(() => {
                nextText.style.opacity = '1';

                currentIndex = nextIndex;

                setTimeout(animateText, duration);
            }, 100);
        }, duration);
    }

    animateText();
}

window.addEventListener('load', animateTextLoop);

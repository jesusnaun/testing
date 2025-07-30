// wwwroot/js/login-interop.js

window.LoginInterop = {

 
    // --- Floating Labels ---
    initFloatingLabels: function (inputGroupIds) {
        inputGroupIds.forEach(groupId => {
            const group = document.getElementById(groupId);
            if (group) {
                const input = group.querySelector('input');
                if (input) {
                    // Check initial value (e.g., for autofill)
                    if (input.value.trim()) {
                        group.classList.add('active');
                    }

                    // Define handlers to be able to remove them later
                    const focusHandler = () => {
                        group.classList.add('active');
                    };
                    const blurHandler = () => {
                        if (!input.value.trim()) {
                            group.classList.remove('active');
                        }
                    };

                    input.addEventListener('focus', focusHandler);
                    input.addEventListener('blur', blurHandler);

                    // Store handlers on the input element for later disposal
                    if (!input._floatingLabelHandlers) {
                        input._floatingLabelHandlers = {};
                    }
                    input._floatingLabelHandlers.focus = focusHandler;
                    input._floatingLabelHandlers.blur = blurHandler;
                }
            } else {
                console.warn(`LoginInterop: Input group with ID '${groupId}' not found for floating labels.`);
            }
        });
    },

    disposeFloatingLabels: function (inputGroupIds) {
        inputGroupIds.forEach(groupId => {
            const group = document.getElementById(groupId);
            if (group) {
                const input = group.querySelector('input');
                if (input && input._floatingLabelHandlers) {
                    input.removeEventListener('focus', input._floatingLabelHandlers.focus);
                    input.removeEventListener('blur', input._floatingLabelHandlers.blur);
                    delete input._floatingLabelHandlers; // Clean up the stored handlers
                }
            }
        });
    }
};
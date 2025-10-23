(() => {
    document.addEventListener('DOMContentLoaded', () => {
        // auto-dismiss Bootstrap alerts with .auto-dismiss
        document.querySelectorAll('.auto-dismiss').forEach(el => {
            setTimeout(() => {
                try { bootstrap.Alert.getOrCreateInstance(el).close(); } catch {}
            }, 2500);
        });

        // generic confirm for forms or buttons with [data-confirm]
        document.querySelectorAll('form[data-confirm]').forEach(f => {
            f.addEventListener('submit', (e) => {
                const msg = f.getAttribute('data-confirm') || 'Are you sure?';
                if (!confirm(msg)) e.preventDefault();
            });
        });
        document.querySelectorAll('button[data-confirm]').forEach(btn => {
            btn.addEventListener('click', (e) => {
                const msg = btn.getAttribute('data-confirm') || 'Are you sure?';
                if (!confirm(msg)) { e.preventDefault(); e.stopPropagation(); }
            });
        });
    });
})();
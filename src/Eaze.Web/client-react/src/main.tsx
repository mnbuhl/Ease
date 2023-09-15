import React from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { createInertiaApp } from '@inertiajs/react';

createInertiaApp({
  resolve: (name) => {
    const pages = import.meta.glob('./pages/**/*.tsx', { eager: true });
    return pages[`./pages/${name}.tsx`];
  },
  setup({ el, App, props }) {
    createRoot(el).render(
      <React.StrictMode>
        <App {...props} />
      </React.StrictMode>
    );
  },
});

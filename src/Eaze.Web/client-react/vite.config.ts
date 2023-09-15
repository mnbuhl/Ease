import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import laravel from 'laravel-vite-plugin';
import { mkdirSync } from 'fs';
import path from 'path';

const outDir = '../wwwroot/dist';

mkdirSync(outDir, { recursive: true });

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    laravel({
      input: ['src/main.tsx'],
      publicDirectory: outDir,
      refresh: true,
    }),
    react(),
  ],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
    },
  },
  build: {
    outDir,
    emptyOutDir: true,
  },
});

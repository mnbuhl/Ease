import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import laravel from 'laravel-vite-plugin';
import { mkdirSync } from 'fs';

const outDir = '../wwwroot';

mkdirSync(outDir, { recursive: true });

// https://vitejs.dev/config/
export default defineConfig(({ ssrBuild }) => {
  return {
    plugins: [
      laravel({
        input: ['src/main.tsx'],
        publicDirectory: outDir,
        buildDirectory: 'dist',
        ssr: ssrBuild ? ['src/ssr.tsx'] : undefined,
        refresh: true,
        ssrOutputDirectory: 'dist',
      }),
      react(),
    ],
    build: {
      emptyOutDir: true,
    },
  };
});

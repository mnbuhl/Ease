import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import laravel from 'laravel-vite-plugin';
import { mkdirSync } from 'fs';

const outDir = '../wwwroot';
const distDir = 'dist';

mkdirSync(outDir, { recursive: true });

// https://vitejs.dev/config/
export default defineConfig(({ ssrBuild }) => {
  return {
    plugins: [
      laravel({
        input: ['src/main.tsx'],
        publicDirectory: outDir,
        hotFile: `${outDir}/${distDir}/hot`,
        buildDirectory: distDir,
        ssr: ssrBuild ? ['src/ssr.tsx'] : undefined,
        refresh: true,
        ssrOutputDirectory: distDir,
      }),
      react(),
    ],
    build: {
      emptyOutDir: true,
    },
  };
});

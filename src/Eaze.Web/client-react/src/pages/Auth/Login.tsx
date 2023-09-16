import { Head } from '@inertiajs/react';
import GuestLayout from '../../layouts/GuestLayout';

const Login = () => {
  return (
    <>
      <Head>
        <title>Login</title>
      </Head>
      <div>
        <h1>LoginPage</h1>
      </div>
    </>
  );
};

Login.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default Login;

import { Head, usePage } from '@inertiajs/react';
import { PageProps } from '../lib/utils/page-props';

const IndexPage = () => {
  const { auth } = usePage<PageProps>().props;

  console.log(auth);

  return (
    <>
      <Head>
        <title>Index</title>
      </Head>
      <div>IndexPage</div>
    </>
  );
};

export default IndexPage;

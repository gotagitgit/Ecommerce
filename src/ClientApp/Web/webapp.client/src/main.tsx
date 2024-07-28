import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import { Inventory } from './inventory/Inventory';

const router = createBrowserRouter([
  {
    path: "/",
    element: (<Inventory />)
  },
  // {
  //   path: "inventory",
  //   element: (<Inventory />)
  // }
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

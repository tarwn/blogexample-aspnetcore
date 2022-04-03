import type { FC } from "react";

export const Screen: FC = ({ children }) => (
  <div className="screen">
    <header>
      <div className="header-content">
        <div className="header-logo"></div>
        <div className="header-links">
          <a href="#">Help</a>
          <a href="#">Logout</a>
        </div>
      </div>
    </header>
    <div className="container">
      <main role="main">
        {children}
      </main>
    </div>

    <footer>
      <div className="footer-content">
        Awesome Footer
      </div>
    </footer>
  </div>
);

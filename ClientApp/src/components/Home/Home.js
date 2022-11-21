import './Home.css'

export const Home = () => {
    return (
        <div className="container-fluid pt-4 px-4 h-100">
            <div className="row h-100 bg-secondary rounded align-items-center justify-content-center mx-0 mb-4">
                <div className="col-md-6">
                    <div className="home fs-4">
                        <h1 className="fs-1">Welcome!</h1>
                        <p>Welcome to <strong>SmartShopping</strong> - app that allows you to shop in a smarter way.</p>
                        <p>Currently under <i>'heavy'</i> developement. Hopefully that will change soon.</p>
                    </div>
                </div>
            </div>
        </div>
    );
}

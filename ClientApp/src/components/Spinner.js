import { Spinner as ReactstrapSpinner} from 'reactstrap';

export const Spinner = () => {
    return (
        <div className="w-100 h-100 position-relative">
            <div className="position-absolute top-50 start-50 translate-middle">
                <ReactstrapSpinner className="text-primary" style={{width: "5rem", height: "5rem"}} role="status"/>
            </div>
        </div>
    );
}
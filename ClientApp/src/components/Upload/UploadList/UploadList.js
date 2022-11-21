import { useSelector } from "react-redux";
import { asyncStatus } from "../../../state/AsyncStatus";
import { uploadProductsSelector, uploadProductsStatusSelector } from "../../../state/slices/UploadProductsSlice";
import { Spinner } from "../../Spinner";
import { UploadListItem } from "./UploadListItem";

export const UploadList = (props) => {
    const uploadList = useSelector(uploadProductsSelector);
    const productsStatus = useSelector(uploadProductsStatusSelector);

    if (productsStatus == asyncStatus.LOADING) {
        return (
            <Spinner/>
        );
    }

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                {uploadList.map((item, index) => <UploadListItem item={item} key={index} shops={props.shops}/>)}
            </div>
        </div>
    );
}
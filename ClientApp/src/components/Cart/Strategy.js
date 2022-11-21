import { useState } from "react";
import { Tag } from "../Tag/Tag";

export const Strategy = (props) => {
    const [expanded, setExpanded] = useState(false);

    const strategy = props.strategy;

    let state = 'Full';

    if (strategy.products.length === 0)
        state = 'Empty';
    else if (strategy.unavailableProducts.length > 0)
        state = 'Incomplete';

    return (
        <div className = "col-sm-12 col-md-6 col-xl-4">
            <div className="bg-secondary rounded align-items-center justify-content-between p-4">
                <div className="d-flex align-items-center justify-content-between mb-2">
                    <h4>{strategy.shop}</h4>
                    <span style={{fontSize: "24px"}} onClick={() => setExpanded(!expanded)}>
                        { expanded ? <i className="fa fa-angle-up cartable-arrow-icon"/> : <i className="fa fa-angle-down cartable-arrow-icon"/> }
                    </span>
                </div>
                <div className="d-flex align-items-center justify-content-between">
                    <h4><span style={{ color: "gray", fontWeight: "lighter"}}>Total: </span>{strategy.price.toFixed(2)} €</h4>
                    <Tag type={`strategy-${state.toLowerCase()}`} name={state}/>
                </div>
                {expanded && (
                    <div className="d-flex justify-content-between mb-0 mt-2">
                        <ul className="w-50">
                            <h6>Available:</h6>
                            {strategy.products.map((p, i) => (
                                <li key={i}>
                                    <div className="d-flex w-75 justify-content-between">
                                        <span>{p.name}</span>
                                        <span>{p.unitPrice.toFixed(2)}€ x{p.amount}</span>
                                    </div>
                                </li>
                            ))}
                        </ul>
                        <ul className="w-50">
                            <h6>Not found:</h6>
                            {strategy.unavailableProducts.map((p, i) => <li key={i}>{p.name}</li>)}
                        </ul>
                    </div>
                )}
            </div>
        </div>
    );
}
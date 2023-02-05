import FetchModule from "./modules/FetchModule";
import SummariseModule from "./modules/SummariseModule";

export default function ComponentEdit(props) {
  return (
    <For each={props.functions}>
      {(func, i) => {
        return (
          <div>
            {() => {
              switch (func.function) {
                case "fetch":
                  return (
                    <FetchModule
                      index={i}
                      setFunctions={props.setFunctions}
                    />
                  );
                case "summarise":
                  return (
                    <SummariseModule
                      index={i}
                      setFunctions={props.setFunctions}
                    />
                  )
              }
            }}
          </div>
        );
      }}
    </For>
  );
}
